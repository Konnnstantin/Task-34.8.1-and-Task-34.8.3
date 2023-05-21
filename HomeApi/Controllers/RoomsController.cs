using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;

        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
      
        /// <summary>
        /// Получение всех  комнат
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _repository.GetRooms();

            var res = new GetRoomsResponse()
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };
            return StatusCode(200, res);
        }

        /// <summary>
        /// Редактирование комнаты
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> EditRoom([FromRoute] Guid id, [FromBody] EditRoomRequest request)
        {
            var withSameName = await _repository.GetRoomByName(request.Name);
            if (withSameName == null)
                return StatusCode(400, $"Комната с таким названием не существует{request.Name}. Введите имя комнаты.");

            await _repository.UpdateRoom(withSameName, new UpdateRoomQuery(request.Name,request.Area,request.GasConnected,request.Voltage));

            return StatusCode(200, $"Устройство обновлено! Имя - {withSameName.Name}, Серийный номер - {withSameName.Id}");
        }
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }

            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }
    }
}