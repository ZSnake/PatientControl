using System.Collections.Generic;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using AutoMapper;
using ClassLibrary1;
using NHibernate.Util;
using PatientControl.Web.Models;

namespace PatientControl.Web.Controllers
{
    public class RoomsController : Controller
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public RoomsController()
        {
            
        }

        public RoomsController(IReadOnlyRepository readOnlyRepository, IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [GET("/rooms")]
        public IEnumerable<RoomModel> GetAll()
        {
            var rooms = _readOnlyRepository.GetAll<Room>();
            return _mappingEngine.Map<IEnumerable<Room>, IEnumerable<RoomModel>>(rooms);
        }
    }
}