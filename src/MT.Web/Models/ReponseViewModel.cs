using System.Collections.Generic;

namespace MT.Web.Models
{
    public class ReponseViewModel<T>
    {
        public bool Success { get; set; }
        public IEnumerable<T> Data { get; set; }
        public ResponseErrorMessagesCaminhaoViewModel Errors { get; set; }
    }
}
