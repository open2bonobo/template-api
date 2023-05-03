import React from "react";
import { TodoItem } from "../types";
import { todoPriorityLabelsMap, todoStatusLabelsMap } from "../store/constants";

type Props = {
  task: TodoItem;
  onDelete: (id: number) => void;
  onEdit: (task: TodoItem) => void;
};

const Task = ({ task, onDelete, onEdit }: Props) => {
  return (
    <div className="card my-2">
      <ul className="list-group p-2">
        <li className="card-title"><span className="fw-bolder">Name:</span> {task.name}</li>
        <li><span className="fw-bolder">Description:</span> {task.description}</li>
        <li value={task.priority}>
          {" "}
          <span className="fw-bolder">Priority:</span> {todoPriorityLabelsMap[task.priority]}
        </li>
        <li value={task.status}><span className="fw-bolder">Status:</span> {todoStatusLabelsMap[task.status]}</li>
      </ul>
      <div className="card-body text-end">
        {task.status !== 2 ? (
          <button className="btn btn-light card-link col-2" onClick={() => onEdit(task)}>
            EDIT
          </button>
        ) : (
          ""
        )}
        <button className="btn btn-dark card-link col-2" onClick={() => onDelete(task.id)}>
          DELETE
        </button>
      </div>
    </div>
  );
};

export default Task;
