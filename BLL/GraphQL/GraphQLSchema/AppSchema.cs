using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.GraphQL.GraphQLQueries;
using DAL.Interface;
using DataModel.Data;
using GraphQL;
using GraphQL.Types;

namespace BLL.GraphQL.GraphQLSchema
{
    // GraphQL AppSchema類別繼承自Schema類別
    public class AppSchema : Schema , ISchema
    {
        // 在建構式引入IDependencyResolver類別，以解析/處理Client Request事件
        public AppSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            // IDependencyResolver的Query成員，用來解析/處理Client Request事件之Query(Get)動作，
            // 我們要指定用來處理Query動作之類別，本例為AppQuery。
            Query = resolver.Resolve<AppQuery>();
        }
    }
}