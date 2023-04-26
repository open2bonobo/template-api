using System.Net;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Backend.Repository;
using System.Net.Http.Json;
using Backend.Controllers;
using Backend.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Backend.Profiles;
using System.Text;
using System.Text.Json;

namespace Backend.Tests.Integration
{
    public class TaskControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private IRepository<Backend.Models.Task> _repository;
        private readonly WebApplicationFactory<Program> _factory;
        private IMapper _mapper;
        private TaskController _controller;

        public TaskControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            // Initialize dependencies
            _repository = A.Fake<IRepository<Backend.Models.Task>>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new TaskProfile())).CreateMapper();

            // Initialize controller
            _controller = new TaskController(_repository, _mapper);
        }
        [Fact]
        public async Task GetAll_ReturnsTasks()
        {
            // given

            // when
            var response = await _client.GetAsync("api/task");

            // then
            response.IsSuccessStatusCode.Should().BeTrue();
        }
        [Fact]
        public async Task GetById_ReturnsTask()
        {
            // Arrange
            var taskId = 1;
            var expectedTask = new Backend.Models.Task { Id = taskId, Name = "Task 1", Description = "Description 1", Priority = 2, Status = Backend.Models.Status.InProgress };
            A.CallTo(() => _repository.GetByIdAsync(taskId)).Returns(expectedTask);
            var controller = new TaskController(_repository, _mapper);
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/tasks/{taskId}");

            // Act
            var response = await controller.GetById(taskId) as OkObjectResult;
            var actualTask = response.Value as TaskReadDto;

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            actualTask.Should().NotBeNull();
            actualTask.Id.Should().Be(expectedTask.Id);
            actualTask.Name.Should().Be(expectedTask.Name);
            actualTask.Priority.Should().Be(expectedTask.Priority);
            actualTask.Status.Should().Be(expectedTask.Status);

        }
        [Fact]
        public async Task Create_ReturnsTask()
        {

            // given
            var input = new TaskCreateDto() { Name = "Task 1", Description = "Description 1", Priority = 2, Status = Backend.Models.Status.Initial };


            // when
            var response = await _client.PostAsJsonAsync("api/task", input);

            // then
            response.IsSuccessStatusCode.Should().BeTrue();

        }
        [Fact]
        public async Task Update_ReturnsTask()
        {

            // given
            var input = new TaskUpdateDto() { Id = 1, Name = "Task 1", Description = "Description 1", Priority = 2, Status = Backend.Models.Status.Initial };

            var content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            // when
            var response = await _client.PutAsync($"api/task/{input.Id}", content);

            // then
            response.IsSuccessStatusCode.Should().BeTrue();

        }
        [Fact]
        public async Task Delete_ReturnsTask()
        {

            // given
            var input = new TaskDeleteDto() { Id = 1, Name = "Task 1", Description = "Description 1", Priority = 2, Status = Backend.Models.Status.Initial };

            // when
            var response = await _client.DeleteAsync($"api/task/{input.Id}");

            // then
            response.IsSuccessStatusCode.Should().BeTrue();

        }
    }
}
