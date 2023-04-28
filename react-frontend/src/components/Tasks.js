import React from "react";
import Task from "./Task";

const Tasks = ({ tasks, onDelete, onEdit, optionsPriority, optionsStatus }) => {
  return (
    <>
      {tasks.map((task) => (
        <Task
          key={task.id}
          task={task}
          onDelete={onDelete}
          onEdit={onEdit}
          optionsPriority={optionsPriority}
          optionsStatus={optionsStatus}
        />
      ))}
    </>
  );
};

export default Tasks;
