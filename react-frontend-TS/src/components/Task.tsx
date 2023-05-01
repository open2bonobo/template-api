import React  from "react";
import { TodoItem } from "../types";
import { todoPriorityLabelsMap, todoStatusLabelsMap } from "../store/constants";

type Props = {
  task: TodoItem
  onDelete: (id: number) => void
  onEdit: (task: TodoItem) => void
}

const Task = ({ task, onDelete, onEdit }: Props) => {

  return (
    <div className="task">
      <ul className="task">
        <li>Name: {task.name}</li>
        <li>Description: {task.description}</li>
        <li value={task.priority}> Priority: {todoPriorityLabelsMap[task.priority]}</li>
        <li value={task.status}>Status: {todoStatusLabelsMap[task.status]}</li>
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
