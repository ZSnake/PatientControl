using AutoMapper;
using ClassLibrary1;
using FizzWare.NBuilder;
using Machine.Specifications;
using PatientControl.Web.Models;

namespace PatientControl.Web.Tests.Mapping
{
    public class when_mapping_a_room_to_a_model
    {
        static IMappingEngine _mappingEngine;
        static Room _room;
        static RoomModel _expectedModel;
        static RoomModel _result;

        Establish context =
            () =>
                {
                    new ConfigureAutoMapper().Run();
                    _mappingEngine = Mapper.Engine;
                    _room = Builder<Room>.CreateNew().Build();
                    _expectedModel = new RoomModel
                                         {
                                             Name = _room.Name
                                         };
                };

        Because of =
            () => _result = _mappingEngine.Map<Room, RoomModel>(_room);

        It should_return_the_expected_model =
            () => _result.ShouldBeLike(_expectedModel);
    }
}