using ActionCommandGame.Services.Model.Core;

namespace ActionCommandGame.Services.Extensions
{
    public static class ServiceResultExtensions
    {
        public static T NotFound<T>(this T serviceResult)
            where T: ServiceResult
        {
            var message = new ServiceMessage
            {
                Code = "NotFound",
                Message = "These aren't the droids you are looking for. (hand wave) Please move along.",
                MessagePriority = MessagePriority.Error
            };
            serviceResult.Messages.Add(message);

            return serviceResult;
        }

        public static T PlayerNotFound<T>(this T serviceResult)
            where T : ServiceResult
        {
            var message = new ServiceMessage
            {
                Code = "PlayerNotFound",
                Message = "Who? I don't know this player. Come back with valid identification.",
                MessagePriority = MessagePriority.Error
            };
            serviceResult.Messages.Add(message);

            return serviceResult;
        }

        public static T ItemNotFound<T>(this T serviceResult)
            where T : ServiceResult
        {
            var message = new ServiceMessage
            {
                Code = "ItemNotFound",
                Message = "What? I don't know what item you are talking about. Come back when you have more information about this item.",
                MessagePriority = MessagePriority.Error
            };
            serviceResult.Messages.Add(message);

            return serviceResult;
        }

        public static T NotEnoughMoney<T>(this T serviceResult)
            where T : ServiceResult
        {
            var message = new ServiceMessage
            {
                Code = "NotEnoughMoney",
                Message = "You cannot afford that.",
                MessagePriority = MessagePriority.Error
            };
            serviceResult.Messages.Add(message);

            return serviceResult;
        }

        public static T WithMessages<T>(this T serviceResult,
            IList<ServiceMessage> messages)
            where T : ServiceResult
        {
            foreach (var serviceMessage in messages)
            {
                serviceResult.Messages.Add(serviceMessage);
            }

            return serviceResult;
        }
    }
}
