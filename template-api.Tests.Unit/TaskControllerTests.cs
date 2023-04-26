using AutoMapper;
using Backend.Controllers;
using Backend.Dtos;
using Backend.Profiles;
using Backend.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back_end.Tests
{

    public class TaskControllerTests
    {
        private TaskController _controller;
        private IRepository<Backend.Models.Task> _repository;
        private IMapper _mapper;


        public TaskControllerTests()
        {
            // Initialize dependencies
            _repository = A.Fake<IRepository<Backend.Models.Task>>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new TaskProfile())).CreateMapper();

            // Initialize controller
            _controller = new TaskController(_repository, _mapper);

        }
        [Fact]
        public async Task GetAll_ReturnsOkObjectResult()
        {
            // Arrange
            var tasks = new List<Backend.Models.Task>
        {
            new Backend.Models.Task() {
                        Id = 0,
                        Name = "Woshing",
                        Priority = 1,
                        Status = Backend.Models.Status.Completed
                    },
                    new Backend.Models.Task() {
                        Id = 0,
                        Name = "Tooth",
                        Priority = 1,
                        Status = Backend.Models.Status.InProgress
                    },
                    new Backend.Models.Task() {
                        Id = 0,
                        Name = "Breakfast",
                        Priority = 1,
                        Status = Backend.Models.Status.Initial
                    },
                    new Backend.Models.Task() {
                        Id = 0,
                        Name = "HorsingRound",
                        Priority = 1,
                        Status = Backend.Models.Status.Initial
                    }
        };

            _repository = A.Fake<IRepository<Backend.Models.Task>>();
            A.CallTo(() => _repository.GetAllAsync()).Returns(tasks);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TaskProfile>();
            });

            _mapper = config.CreateMapper();

            _controller = new TaskController(_repository, _mapper);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<TaskReadDto>>();

            var taskDtos = okResult.Value as IEnumerable<TaskReadDto>;
            taskDtos.Should().HaveCount(tasks.Count);
        }
        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenTasksExist()
        {
            // Arrange
            var fakeTasks = A.CollectionOfDummy<Backend.Models.Task>(3).ToList();
            A.CallTo(() => _repository.GetAllAsync()).Returns(fakeTasks);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var tasks = result.As<OkObjectResult>().Value.Should().BeAssignableTo<IEnumerable<TaskReadDto>>().Subject;
            tasks.Should().HaveCount(fakeTasks.Count);
            tasks.Should().OnlyContain(t => fakeTasks.Any(ft => ft.Id == t.Id));
        }
        [Fact]
        public async Task GetAll_ReturnsNotFoundResult_WhenNoTasksExist()
        {
            // Arrange
            A.CallTo(() => _repository.GetAllAsync()).Returns(new List<Backend.Models.Task>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetAll_ReturnsInternalServerError_WhenRepositoryThrowsException()
        {
            // Arrange
            var errorMessage = "An error occurred while fetching tasks";
            A.CallTo(() => _repository.GetAllAsync()).Throws(new Exception(errorMessage));

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            // result.As<ObjectResult>().Value.Should().Be($"An error occurred while fetching tasks: {errorMessage}");

        }
        [Fact]
        public async Task GetById_ReturnsOk_WhenTaskExists()
        {
            // Arrange
            var taskId = 0;
            var task = new Backend.Models.Task()
            {
                Id = 0,
                Name = "Woshing",
                Priority = 1,
                Status = Backend.Models.Status.Completed
            };
            var taskReadDto = new TaskReadDto()
            {
                Id = 0,
                Name = "Woshing",
                Priority = 1,
                Status = Backend.Models.Status.Completed
            };
            _mapper = A.Fake<IMapper>();
            A.CallTo(() => _repository.GetByIdAsync(taskId)).Returns(task);
            A.CallTo(() => _mapper.Map<TaskReadDto>(task)).Returns(taskReadDto);

            // Act
            var result = await _controller.GetById(taskId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(taskReadDto);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenTaskDoesNotExist()
        {
            // Arrange
            var taskId = 1;
            A.CallTo(() => _repository.GetByIdAsync(taskId)).Returns((Backend.Models.Task)null);

            // Act
            var result = await _controller.GetById(taskId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            result.As<NotFoundResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
        [Fact]
        public async Task GetById_ReturnsInternalServerError_WhenRepositoryThrowsException()
        {
            // Arrange
            var taskId = 1;
            var errorMessage = "An error occurred while fetching task";
            A.CallTo(() => _repository.GetByIdAsync(taskId)).Throws(new Exception(errorMessage));

            // Act
            var result = await _controller.GetById(taskId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithValidInput()
        {
            // Arrange
            var input = new TaskCreateDto()
            {
                Name = "name",
                Priority = 5,
                Status = Backend.Models.Status.Completed
            };
            var task = new Backend.Models.Task()
            {
                Id = 3,
                Name = "name",
                Priority = 5,
                Status = Backend.Models.Status.Completed
            };
            var taskReadDto = new TaskReadDto()
            {
                Id = 3,
                Name = "name",
                Priority = 5,
                Status = Backend.Models.Status.Completed
            };

            var id = 3;

            A.CallTo(() => _repository.CreateAsync(A<Backend.Models.Task>._)).Returns(Task.CompletedTask);
            A.CallTo(() => _repository.SaveChanges()).Returns(true);
            A.CallTo(() => _repository.GetByIdAsync(id)).Returns(task);

            _mapper = A.Fake<IMapper>();
            A.CallTo(() => _mapper.Map<Backend.Models.Task>(input)).Returns(task);
            A.CallTo(() => _mapper.Map<TaskReadDto>(task)).Returns(taskReadDto);

            var controller = new TaskController(_repository, _mapper);

            // Act
            var result = await controller.Create(input);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.ActionName.Should().Be(nameof(TaskController.GetById));
            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.RouteValues["id"].Should().Be(taskReadDto.Id);
            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.Value.Should().Be(taskReadDto);
        }
        [Fact]
        public async Task Create_ReturnsInternalServerError_WhenRepositorySaveChangesReturnsFalse()
        {
            // Arrange
            var input = new TaskCreateDto()
            {
                Name = "name",
                Priority = 5,
                Status = Backend.Models.Status.Completed
            };
            var task = new Backend.Models.Task()
            {
                Id = 3,
                Name = "name",
                Priority = 5,
                Status = Backend.Models.Status.Completed
            };

            var _mapper = A.Fake<IMapper>();
            A.CallTo(() => _mapper.Map<Backend.Models.Task>(input)).Returns(task);

            // Act
            var result = await _controller.Create(input);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task Create_ReturnsInternalServerError_WhenRepositoryThrowsException()
        {
            // Arrange
            var input = new TaskCreateDto()
            {
                Name = "name",
                Priority = 5,
                Status = Backend.Models.Status.Completed
            };
            string exceptionMessage = "Failed to save changes to the database.";
            A.CallTo(() => _repository.CreateAsync(A<Backend.Models.Task>._)).Throws(new Exception(exceptionMessage));

            var mapper = A.Fake<IMapper>();

            var controller = new TaskController(_repository, mapper);

            // Act
            var result = await controller.Create(input);

            // Assert
            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            result.Should().BeOfType<ObjectResult>().Which.Value.Should().Be($"An error occurred while creating the task: {exceptionMessage}");
        }
        [Fact]
        public async Task Update_WithValidInput_ReturnsOkResult()
        {
            // Arrange
            int id = 1;
            TaskUpdateDto input = new TaskUpdateDto { Name = "Updated Task", Priority = 2, Status = Backend.Models.Status.InProgress };
            Backend.Models.Task existingTask = new Backend.Models.Task { Id = id, Name = "Task 1", Priority = 1, Status = Backend.Models.Status.Completed };
            Backend.Models.Task updatedTask = new Backend.Models.Task { Id = id, Name = input.Name, Priority = input.Priority, Status = input.Status };
            TaskReadDto expectedDto = _mapper.Map<TaskReadDto>(updatedTask);

            A.CallTo(() => _repository.GetByIdAsync(id)).Returns(existingTask);
            A.CallTo(() => _repository.UpdateAsync(existingTask));
            A.CallTo(() => _repository.SaveChanges()).Returns(Task.FromResult(true));

            // Act
            IActionResult result = await _controller.Update(id, input);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(expectedDto);
        }
        [Fact]
        public async Task Update_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            TaskUpdateDto input = new TaskUpdateDto { Name = "Updated Task", Priority = 2, Status = Backend.Models.Status.InProgress };

            A.CallTo(() => _repository.GetByIdAsync(id)).Returns<Backend.Models.Task>(null);

            // Act
            IActionResult result = await _controller.Update(id, input);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task Update_WithInvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            int id = 1;
            TaskUpdateDto input = new TaskUpdateDto { Name = "", Priority = 6, Status = Backend.Models.Status.InProgress };
            Backend.Models.Task existingTask = new Backend.Models.Task { Id = id, Name = "Task 1", Priority = 1, Status = Backend.Models.Status.Completed };

            A.CallTo(() => _repository.GetByIdAsync(id)).Returns(existingTask);

            // Act
            IActionResult result = await _controller.Update(id, input);

            // Assert
            result.Should().BeOfType<ObjectResult>();
        }
        [Fact]
        public async Task Update_ValidIdAndInput_ShouldReturnOkWithUpdatedTask()
        {
            // Arrange
            var id = 1;
            var input = new TaskUpdateDto { Name = "New Task", Priority = 2, Status = Backend.Models.Status.InProgress };
            var existingTask = new Backend.Models.Task { Id = id, Name = "Old Task", Priority = 3, Status = Backend.Models.Status.Completed };
            var updatedTask = new Backend.Models.Task { Id = id, Name = input.Name, Priority = input.Priority, Status = input.Status };
            A.CallTo(() => _repository.GetByIdAsync(id)).Returns(existingTask);
            A.CallTo(() => _repository.UpdateAsync(A<Backend.Models.Task>.Ignored)).DoesNothing();
            A.CallTo(() => _repository.SaveChanges()).Returns(true);

            // Act
            var result = await _controller.Update(id, input);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(_mapper.Map<TaskReadDto>(updatedTask));
        }
        [Fact]
        public async Task Update_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var id = 1;
            var input = new TaskUpdateDto { Name = "New Task", Priority = 2, Status = Backend.Models.Status.InProgress };

            A.CallTo(() => _repository.GetByIdAsync(id)).Returns<Backend.Models.Task>(null);

            // Act
            var result = await _controller.Update(id, input);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task Update_SaveChangesFails_ShouldReturnInternalServerError()
        {
            // Arrange
            var id = 1;
            var input = new TaskUpdateDto { Name = "New Task", Priority = 2, Status = Backend.Models.Status.InProgress };
            var existingTask = new Backend.Models.Task { Id = id, Name = "Old Task", Priority = 3, Status = Backend.Models.Status.Completed };
            A.CallTo(() => _repository.GetByIdAsync(id)).Returns(existingTask);
            A.CallTo(() => _repository.UpdateAsync(A<Backend.Models.Task>.Ignored)).DoesNothing();
            A.CallTo(() => _repository.SaveChanges()).Returns(false);

            // Act
            var result = await _controller.Update(id, input);

            // Assert
            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task Update_ExceptionThrown_ShouldReturnInternalServerError()
        {
            // Arrange
            var id = 1;
            var input = new TaskUpdateDto { Name = "New Task", Priority = 2, Status = Backend.Models.Status.InProgress };
            A.CallTo(() => _repository.GetByIdAsync(id)).Throws<Exception>();

            // Act
            var result = await _controller.Update(id, input);

            // Assert
            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task Delete_WithValidId_DeletesTaskAndReturnsOk()
        {
            // Arrange
            var taskId = 1;
            Backend.Models.Task task = new Backend.Models.Task { Id = taskId, Name = "Task 1", Priority = 1, Status = Backend.Models.Status.InProgress };
            A.CallTo(() => _repository.GetByIdAsync(taskId)).Returns(task);
            A.CallTo(() => _repository.DeleteAsync(task));
            // Act
            var result = await _controller.Delete(taskId);

            // Assert
            A.CallTo(() => _repository.DeleteAsync(task)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();

            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var taskId = 1;
            A.CallTo(() => _repository.GetByIdAsync(taskId)).Returns<Backend.Models.Task>(null);

            // Act
            var result = await _controller.Delete(taskId);

            // Assert
            A.CallTo(() => _repository.DeleteAsync(A<Backend.Models.Task>._)).MustNotHaveHappened();
            A.CallTo(() => _repository.SaveChanges()).MustNotHaveHappened();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_WithValidId_FailsToDelete_ReturnsInternalServerError()
        {
            // Arrange
            var taskId = 1;
            var task = new Backend.Models.Task { Id = taskId, Name = "Task 1", Priority = 1, Status = Backend.Models.Status.InProgress };
            A.CallTo(() => _repository.GetByIdAsync(taskId)).Returns(task);
            A.CallTo(() => _repository.DeleteAsync(task));
            A.CallTo(() => _repository.SaveChanges()).Returns(Task.FromResult(false));

            // Act
            var result = await _controller.Delete(taskId);

            // Assert
            A.CallTo(() => _repository.DeleteAsync(task)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();

            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task Delete_WithValidId_ThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int id = 1;
            A.CallTo(() => _repository.GetByIdAsync(id)).Throws(new Exception("An error occurred while deleting the task."));

            // Act
            var result = await _controller.Delete(id);

            // Assert
            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        // Test case for deleting an existing task
        [Fact]
        public async Task Delete_WithExistingTask_DeletesTaskAndReturnsOk()
        {
            // Arrange
            int taskId = 1;
            var existingTask = new Backend.Models.Task
            {
                Id = taskId,
                Name = "Task 1",
                Priority = 2,
                Status = Backend.Models.Status.InProgress
            };

            A.CallTo(() => _repository.GetByIdAsync(taskId))
                .Returns(existingTask);

            A.CallTo(() => _repository.DeleteAsync(existingTask))
                .DoesNothing();

            A.CallTo(() => _repository.SaveChanges())
                .Returns(true);

            var controller = new TaskController(_repository, _mapper);

            // Act
            var result = await controller.Delete(taskId);

            // Assert
            A.CallTo(() => _repository.DeleteAsync(existingTask)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(existingTask);
        }

        // Test case for deleting a non-existing task
        [Fact]
        public async Task Delete_WithNonExistingTask_ReturnsNotFound()
        {
            // Arrange
            int taskId = 1;

            A.CallTo(() => _repository.GetByIdAsync(taskId)).Returns<Task<Backend.Models.Task>>(null);

            var controller = new TaskController(_repository, _mapper);

            // Act
            var result = await controller.Delete(taskId);

            // Assert
            A.CallTo(() => _repository.DeleteAsync(A<Backend.Models.Task>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _repository.SaveChanges()).MustNotHaveHappened();

            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        // Test case for failing to save changes to the database after deleting a task
        [Fact]
        public async Task Delete_WithValidInput_FailsToDeleteTask_ReturnsInternalServerError()
        {
            // Arrange
            int taskId = 1;
            var existingTask = new Backend.Models.Task
            {
                Id = taskId,
                Name = "Task 1",
                Priority = 2,
                Status = Backend.Models.Status.InProgress
            };

            A.CallTo(() => _repository.GetByIdAsync(taskId))
                .Returns(existingTask);

            A.CallTo(() => _repository.DeleteAsync(existingTask))
                .DoesNothing();

            A.CallTo(() => _repository.SaveChanges())
                .Returns(false);

            var controller = new TaskController(_repository, _mapper);

            // Act
            var result = await controller.Delete(taskId);

            // Assert
            A.CallTo(() => _repository.DeleteAsync(existingTask)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();

            result.Should().BeOfType<ObjectResult>();
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

    }
}
