import React from "react";


const Task = ({ task, onDelete, onEdit, optionsPriority, optionsStatus }) => {
  return (
    <div className="task">
      <ul className="task">
        <li>Name: {task.name}</li>
        <li>Description: {task.description}</li>
        <li value={task.priority}> Priority: {optionsPriority.find((x)=>x.value == task.priority).label}</li>
        <li value={task.status}>Status: {optionsStatus.find((t)=>t.value == task.status).label}</li>
        {task.status !== 2 ? (
          <button className="btn" onClick={() => onEdit(task)}>
            EDIT
          </button>
        ) : (
          ""
        )}
        <button className="btn" onClick={() => onDelete(task.id)}>
          DELETE
        </button>
      </ul>
    </div>
  );
};

export default Task;
