using System;
using System.Collections.Generic;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Groups
{
    /// <summary>
    /// Учебная группа
    /// </summary>
    public class Group : Entity
    {
        public Guid Uid { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Year { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public int MaxStudents { get; private set; }
        public GroupStatus Status { get; private set; }
        public Guid? CuratorUid { get; private set; }
        public Guid DepartmentUid { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
        
        private Group() { }
        
        public static Result<Group> Create(
            string code,
            string name,
            string description,
            int year,
            DateTime startDate,
            int maxStudents,
            Guid departmentUid,
            Guid? curatorUid = null)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<Group>(GroupErrors.EmptyGroupCode);
            
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Group>(GroupErrors.EmptyGroupName);
            
            if (maxStudents <= 0)
                return Result.Failure<Group>(GroupErrors.InvalidMaxStudents);
            
            var group = new Group
            {
                Uid = Guid.NewGuid(),
                Code = code.Trim(),
                Name = name.Trim(),
                Description = description,
                Year = year,
                StartDate = startDate,
                MaxStudents = maxStudents,
                Status = GroupStatus.Active,
                DepartmentUid = departmentUid,
                CuratorUid = curatorUid,
                CreatedAtUtc = DateTime.UtcNow
            };
            
            group.Raise(new GroupCreatedDomainEvent(group.Uid));
            return group;
        }
        
        public void UpdateDetails(string name, string description, int maxStudents)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя группы не может быть пустым");
            
            if (maxStudents <= 0)
                throw new ArgumentException("Максимальное количество студентов должно быть положительным числом");
            
            Name = name.Trim();
            Description = description;
            MaxStudents = maxStudents;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
        
        public void SetCurator(Guid? curatorUid)
        {
            CuratorUid = curatorUid;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
        
        public void UpdateStatus(GroupStatus status)
        {
            Status = status;
            
            if (status == GroupStatus.Completed && !EndDate.HasValue)
            {
                EndDate = DateTime.UtcNow;
            }
            
            LastModifiedAtUtc = DateTime.UtcNow;
            
            if (status == GroupStatus.Active)
            {
                Raise(new GroupActivatedDomainEvent(Uid));
            }
            else if (status == GroupStatus.Completed)
            {
                Raise(new GroupCompletedDomainEvent(Uid));
            }
        }
        
        public void SetEndDate(DateTime endDate)
        {
            if (endDate <= StartDate)
                throw new ArgumentException("Дата окончания должна быть позже даты начала");
            
            EndDate = endDate;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
} 