using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Viridisca.Modules.Grading.Application.Services;
using Viridisca.Modules.Grading.Domain.Models;
using Viridisca.Modules.Grading.Domain.Repositories;

namespace Viridisca.Tests.Modules.Grading.Unit
{
    public class GradeRevisionsServiceTests
    {
        private readonly Mock<IGradeRevisionsRepository> _mockRepository;
        private readonly GradeRevisionsService _service;
        private readonly Guid _gradeUid = Guid.NewGuid();
        private readonly Guid _teacherUid = Guid.NewGuid();

        public GradeRevisionsServiceTests()
        {
            _mockRepository = new Mock<IGradeRevisionsRepository>();
            _service = new GradeRevisionsService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetRevisionsForGradeAsync_ShouldReturnRevisionsFromRepository()
        {
            // Arrange
            var expectedRevisions = new List<GradeRevision>
            {
                new GradeRevision(
                    Guid.NewGuid(),
                    _gradeUid,
                    _teacherUid,
                    80m,
                    85m,
                    "Старое описание",
                    "Новое описание",
                    "Исправление ошибки",
                    DateTime.UtcNow)
            };

            _mockRepository
                .Setup(r => r.GetRevisionsForGradeAsync(_gradeUid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRevisions);

            // Act
            var result = await _service.GetRevisionsForGradeAsync(_gradeUid);

            // Assert
            Assert.Equal(expectedRevisions, result);
            _mockRepository.Verify(r => r.GetRevisionsForGradeAsync(_gradeUid, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetLatestRevisionAsync_ShouldReturnLatestRevisionFromRepository()
        {
            // Arrange
            var expectedRevision = new GradeRevision(
                Guid.NewGuid(),
                _gradeUid,
                _teacherUid,
                80m,
                85m,
                "Старое описание",
                "Новое описание",
                "Исправление ошибки",
                DateTime.UtcNow);

            _mockRepository
                .Setup(r => r.GetLatestRevisionForGradeAsync(_gradeUid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRevision);

            // Act
            var result = await _service.GetLatestRevisionAsync(_gradeUid);

            // Assert
            Assert.Equal(expectedRevision, result);
            _mockRepository.Verify(r => r.GetLatestRevisionForGradeAsync(_gradeUid, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateRevisionAsync_ShouldAddRevisionToRepository()
        {
            // Arrange
            decimal previousValue = 80m;
            string previousDescription = "Старое описание";
            decimal newValue = 85m;
            string newDescription = "Новое описание";
            string revisionReason = "Исправление ошибки";

            // Создаем моковый объект Grade для теста
            var mockGrade = new Mock<Grade>();
            mockGrade.Setup(g => g.Uid).Returns(_gradeUid);

            // Act
            await _service.CreateRevisionAsync(
                mockGrade.Object,
                previousValue,
                previousDescription,
                newValue,
                newDescription,
                _teacherUid,
                revisionReason);

            // Assert
            _mockRepository.Verify(r => r.AddRevisionAsync(
                It.Is<GradeRevision>(rev =>
                    rev.GradeUid == _gradeUid &&
                    rev.TeacherUid == _teacherUid &&
                    rev.PreviousValue == previousValue &&
                    rev.NewValue == newValue &&
                    rev.PreviousDescription == previousDescription &&
                    rev.NewDescription == newDescription &&
                    rev.RevisionReason == revisionReason),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateRevisionAsync_WithNullGrade_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _service.CreateRevisionAsync(
                    null,
                    80m,
                    "Старое описание",
                    85m,
                    "Новое описание",
                    _teacherUid,
                    "Причина"));
        }

        [Fact]
        public async Task CreateRevisionAsync_WithEmptyTeacherUid_ShouldThrowArgumentException()
        {
            // Arrange
            // Создаем моковый объект Grade для теста
            var mockGrade = new Mock<Grade>();
            mockGrade.Setup(g => g.Uid).Returns(_gradeUid);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _service.CreateRevisionAsync(
                    mockGrade.Object,
                    80m,
                    "Старое описание",
                    85m,
                    "Новое описание",
                    Guid.Empty,
                    "Причина"));
        }
    }
} 