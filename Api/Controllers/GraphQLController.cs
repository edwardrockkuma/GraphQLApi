using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using DataModel.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BLL.GraphQL.GraphQLQueries;
using BLL.GraphQL.GraphQLDto;

namespace Api.Controllers
{
   [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        public GraphQLController(ISchema schema, IDocumentExecuter executer)
        {
            _schema = schema;
            _executer = executer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQueryDto query)
        {
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                //_.Inputs = query.Variables?.ToInputs();
                
            }).ConfigureAwait(false);

            if(result.Errors?.Count > 0)
            {
                return Problem(detail: result.Errors.Select(_ => _.Message).FirstOrDefault(), statusCode:500);
            }
            return Ok(result.Data);
        }
    }
}