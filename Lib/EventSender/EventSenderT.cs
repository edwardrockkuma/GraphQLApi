
namespace LibForCore.Event
{

    public class EventSender<T>
    {
        public delegate void PubEventHandler(T e); //定義委託
        private event PubEventHandler onPublish; //定義事件訪問器

        /// <summary>事件發佈</summary>
        public void SendEvent(T e1)
        {
            PubEventHandler handler = onPublish; //防止多緒錯誤
            if (handler != null)
                handler(e1);
        }

        /// <summary>訂閱事件</summary>
        /// <param name='in_onEvent'>觸發事件函式</param>
        public void Register(PubEventHandler in_onEvent)
        {
            onPublish += in_onEvent;
        }

        /// <summary>解除訂閱</summary>
        /// <param name='in_onEvent'>觸發事件函式</param>
        public void UnRegister(PubEventHandler in_onEvent)
        {
            onPublish -= in_onEvent;
        }
    }
}
