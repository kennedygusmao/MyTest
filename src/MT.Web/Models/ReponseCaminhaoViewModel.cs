using System.Collections.Generic;

namespace MT.Web.Models
{
    public class ReponseCaminhaoViewModel<T>
    {
        public bool Success { get; set; }
        public IEnumerable<T> Data { get; set; }
        public ResponseErrorMessagesCaminhaoViewModel Errors { get; set; }

    }

    public class ReponseCaminhaoViewModelData<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public ResponseErrorMessagesCaminhaoViewModel Errors { get; set; }

    }

}
