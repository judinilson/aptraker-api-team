using APTracker.Server.WebApi.Dto.Bag;

namespace APTracker.Server.WebApi.ViewModels.Commands.Bag.Modify
{
    public class BagModifyResponse
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Ответственный
        /// </summary>
        public UserSimplifiedView Responsible { get; set; }
    }
}