using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Groups
{
    /// <summary>
    /// Методы расширения для класса Group
    /// </summary>
    public static class GroupExtensions
    {
        private static readonly string STUDENTS_COUNT_KEY = "CurrentStudentsCount";
        
        /// <summary>
        /// Проверяет, есть ли в группе свободные места для студентов
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns>true, если есть доступные места</returns>
        public static bool HasAvailableCapacity(this Group group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
                
            int currentCount = GetCurrentStudentsCount(group);
            return currentCount < group.MaxStudents;
        }
        
        /// <summary>
        /// Увеличивает счетчик студентов в группе на 1
        /// </summary>
        /// <param name="group">Группа</param>
        public static void IncrementStudentsCount(this Group group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
                
            int currentCount = GetCurrentStudentsCount(group);
            
            if (currentCount >= group.MaxStudents)
                throw new InvalidOperationException($"Достигнуто максимальное количество студентов в группе {group.MaxStudents}");
                
            SetCurrentStudentsCount(group, currentCount + 1);
        }
        
        /// <summary>
        /// Уменьшает счетчик студентов в группе на 1
        /// </summary>
        /// <param name="group">Группа</param>
        public static void DecrementStudentsCount(this Group group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
                
            int currentCount = GetCurrentStudentsCount(group);
            
            if (currentCount <= 0)
                throw new InvalidOperationException("Количество студентов в группе не может быть отрицательным");
                
            SetCurrentStudentsCount(group, currentCount - 1);
        }
        
        private static int GetCurrentStudentsCount(Group group)
        {
            if (!group.TryGetMetadata(STUDENTS_COUNT_KEY, out int count))
            {
                count = 0;
                SetCurrentStudentsCount(group, count);
            }
            
            return count;
        }
        
        private static void SetCurrentStudentsCount(Group group, int count)
        {
            group.SetMetadata(STUDENTS_COUNT_KEY, count);
        }
    }
} 