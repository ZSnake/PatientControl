using System.Collections.Generic;
using AutoMapper;
using ClassLibrary1;
using Machine.Specifications;
using Moq;
using PatientControl.Web.Controllers;
using PatientControl.Web.Models;
using It = Machine.Specifications.It;

namespace PatientControl.Web.Tests
{
    public class when_getting_all_rooms
    {
        static RoomsController _controller;
        static IEnumerable<RoomModel> _result;
        static IReadOnlyRepository _readOnlyRepository;
        static IEnumerable<Room> _listOfRoomsFromRepository;
        static IMappingEngine _mappingEngine;
        static List<RoomModel> _expectedRooms;

        Establish context =
            () =>
                {
                    _readOnlyRepository = Mock.Of<IReadOnlyRepository>();
                    _mappingEngine = Mock.Of<IMappingEngine>();
                    _controller = new RoomsController(_readOnlyRepository, _mappingEngine);

                    _listOfRoomsFromRepository = new List<Room>();
                    Mock.Get(_readOnlyRepository).Setup(x => x.GetAll<Room>()).Returns(_listOfRoomsFromRepository);

                    _expectedRooms = new List<RoomModel>();
                    Mock.Get(_mappingEngine).Setup(
                        x => x.Map<IEnumerable<Room>, IEnumerable<RoomModel>>(_listOfRoomsFromRepository)).Returns(
                            _expectedRooms);
                };

        Because of =
            () => _result = _controller.GetAll();

        It should_return_a_list_of_rooms =
            () => _result.ShouldEqual(_expectedRooms);
    }
}