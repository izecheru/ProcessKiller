# QA_Task

Utility to close a specified process once it reaches the specified lifespan (in minutes)

## How to use

```bash
./QA_Task <process_name> <lifespan_in_minutes> <recheck_frequency>
```

Process name is the name of the process we want to kill

Lifespan in minutes is the amount of minutes a program is allowed to live, if it reaches it, the process will be killed.

Recheck frequency is the amount of minutes it takes for the script to recheck the lifespan and processes with the specified name.

```csharp

```
