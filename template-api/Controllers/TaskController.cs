using AutoMapper;
using Backend.Dtos;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IRepository<Backend.Models.Task> _repository;
        private readonly IMapper _mapper;

        public TaskController(
                                IRepository<Backend.Models.Task> repository,
                                IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _repository.GetAllAsync();
                if (tasks == null || tasks.Count() == 0)
                    return NotFound();

                return Ok(_mapper.Map<IEnumerable<TaskReadDto>>(tasks));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while fetching task with id: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var task = await _repository.GetByIdAsync(id);
                if (task != null)
                    return Ok(_mapper.Map<TaskReadDto>(task));
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while fetching task with id {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateDto input)
        {
            try
            {
                var task = _mapper.Map<Backend.Models.Task>(input);
                await _repository.CreateAsync(task);
                if (await _repository.SaveChanges())
                {
                    var taskReadDto = _mapper.Map<TaskReadDto>(task);
                    return CreatedAtAction(nameof(GetById), new { id = taskReadDto.Id }, taskReadDto);
                }
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save changes to the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the task: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskUpdateDto input)
        {
            try
            {
                var task = await _repository.GetByIdAsync(id);

                if (task == null)
                    return NotFound();

                var updatingTask = _mapper.Map<Backend.Models.Task>(input);

                task.Name = updatingTask.Name;
                task.Priority = updatingTask.Priority;
                task.Status = updatingTask.Status;

                _repository.UpdateAsync(task);
                if (await _repository.SaveChanges())
                {
                    var taskReadDto = _mapper.Map<TaskReadDto>(task);
                    return Ok(taskReadDto);
                }
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save changes to the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the task: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var task = await _repository.GetByIdAsync(id);
                if (task == null)
                    return NotFound();

                _repository.DeleteAsync(task);
                if (await _repository.SaveChanges())
                    return Ok(task);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save changes to the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the task: {ex.Message}");
            }
        }
    }
}