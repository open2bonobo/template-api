import React, { ChangeEvent, useCallback, useState } from "react";
import { RootStore, TodoItem } from "../types";
import { useAppDispatch } from "../store";
import { actions } from "../store/actions";
import { useSelector } from "react-redux";
import { todoPriorityLabelsMap, todoStatusLabelsMap } from "../store/constants";

const AddTask = () => {
  const dispatch = useAppDispatch();
  const task = useSelector((store: RootStore) => store.taskToEdit);

  const handleTaskChange = useCallback(
    ({ field }: { field: keyof TodoItem }) => (
      event: ChangeEvent<HTMLInputElement | HTMLSelectElement>
    ) => {
      const value = event.currentTarget.value;
      dispatch(
        actions.setTaskToEdit({
          task: {
            ...task,
            [field]: value,
          },
        })
      );
    },
    [task, dispatch]
  );

  const onSubmit = (e: ChangeEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (task.id === 0) {
      dispatch(actions.createTask({ task }));
    } else {
      dispatch(actions.updateTask({ task }));
    }
  };

  return (
    <form onSubmit={onSubmit} className="row g-3">
      <div className="col-md-12">
        <label className="form-label">Name</label>
        <input
          className="form-control"
          type="text"
          placeholder="Add Name"
          value={task.name}
          onChange={handleTaskChange({ field: "name" })}
          required
        />
      </div>
      <div className="col-md-12">
        <label className="form-label">Description</label>
        <input
          className="form-control"
          type="text"
          placeholder="Add Description"
          value={task.description}
          onChange={handleTaskChange({ field: "description" })}
          required
        ></input>
      </div>
      <div className="col-md-12">
        <label className="form-label" htmlFor="prioritySelect">
          Select an option:
        </label>
        <select
          id="prioritySelect"
          className="form-select"
          value={task.priority}
          onChange={handleTaskChange({ field: "priority" })}
        >
          {Object.entries(todoPriorityLabelsMap).map((option) => (
            <option
              className="dropdown-item"
              key={+option[0]}
              value={+option[0]}
            >
              {option[1]}
            </option>
          ))}
        </select>
        <p>You selected: {todoPriorityLabelsMap[task.priority]}</p>
      </div>
      <div className="col-md-12">
        <label className="form-label">Select an option:</label>
        <select
          className="form-select"
          value={task.status}
          onChange={handleTaskChange({ field: "status" })}
        >
          {Object.entries(todoStatusLabelsMap).map((option) => (
            <option key={+option[0]} value={+option[0]}>
              {option[1]}
            </option>
          ))}
        </select>
        <p>You selected: {todoStatusLabelsMap[task.status]}</p>
      </div>
      <input type="submit" value="Save Task" className="btn btn-primary"></input>
    </form>
  );
};

export default AddTask;
