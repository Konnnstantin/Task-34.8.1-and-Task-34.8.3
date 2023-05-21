using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApi.Controllers
{
    public class DeleteDeviceRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RoomLocation { get; set; }

    }
}
