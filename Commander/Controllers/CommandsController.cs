using Commander.Data;
using Microsoft.AspNetCore.Mvc;
using Commander.Models;
using AutoMapper;
using Commander.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;

namespace Commander.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommanderController : ControllerBase{
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommanderController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommand()
        {
            return Ok(_mapper.Map<IEnumerable<Command>>(_repository.GetAllCommand()));
        }

        [HttpGet("{id}",Name ="GetCommmandById")]
        public ActionResult<CommandReadDto> GetCommmandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem == null){
                return NotFound();
            }
            var commandReadDto = _mapper.Map<CommandReadDto>(commandItem); 
            return Ok(commandReadDto);
        }
        
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var command = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(command); 
            _repository.SaveChanges();  

            var commanderReadDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute(nameof(GetCommmandById),new {id = commanderReadDto.Id},commanderReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModel = _repository.GetCommandById(id);

            if(commandModel == null){
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandModel);
            _repository.UpdateCommand(commandModel);
            _repository.SaveChanges();
            return NoContent();
        }

        //Partial update of the model
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);

            if(commandModelFromRepo == null){
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);   

            if(!TryValidateModel(commandToPatch)){
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
             var commandModelFromRepo = _repository.GetCommandById(id);

            if(commandModelFromRepo == null){
                return NotFound();
            }

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();

        }
    }
    
}