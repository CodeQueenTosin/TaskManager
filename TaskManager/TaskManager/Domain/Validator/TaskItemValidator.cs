using FluentValidation;
using TaskManager.Domain.Entities;

public class TaskItemValidator : AbstractValidator<TaskItem>
{
    public TaskItemValidator()
    {
        RuleFor(t => t.TaskName)
            .NotEmpty().WithMessage("Task name is required.")
            .MaximumLength(100).WithMessage("Task name must be at most 100 characters.");

        RuleFor(t => t.TaskDescription)
            .NotEmpty().WithMessage("Task description is required.")
            .MaximumLength(500).WithMessage("Task description must be at most 500 characters.");

        RuleFor(t => t.StartDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Start date cannot be in the future.");

        RuleFor(t => t.AllottedTime)
            .GreaterThanOrEqualTo(0).WithMessage("Allotted time must be zero or positive.");

        RuleFor(t => t.ElapsedTime)
            .GreaterThanOrEqualTo(0).WithMessage("Elapsed time must be zero or positive.");

        RuleFor(t => t.TaskStatus)
            .NotNull().WithMessage("Task status must be specified.");
    }
}
