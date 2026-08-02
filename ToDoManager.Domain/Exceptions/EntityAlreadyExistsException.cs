namespace ToDoManager.Domain.Exceptions;
    public class EntityAlreadyExistsException : ApplicationException
    {
        public EntityAlreadyExistsException(string entityName, string parameterName, string? parameterValue) :
            base($"'{entityName}' with '{parameterName}':'{parameterValue}' already exists.")
        {
        }
    }