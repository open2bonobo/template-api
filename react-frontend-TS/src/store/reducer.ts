import { RootStore } from "../types";
import { actions } from "./actions";
import { createReducer } from "@reduxjs/toolkit";

export const reducer = createReducer<RootStore>(
  {
   tasks: [],
    taskToEdit: {
     id: 0,
      name: "default",
      description: "default",
      priority: 0,
      status: 0,
    }
  },
  (builder) => {
    builder
      .addCase(actions.getTasks.fulfilled, (state, action) => {
        state.tasks = action.payload;
      })
      .addCase(actions.updateTask.fulfilled, (state, action) => {
        const otherTasks = state.tasks.filter(({ id }) => id !== action.payload.id)
        state.tasks = [...otherTasks, action.payload];
      })
      .addCase(actions.deleteTask.fulfilled, (state, action) => {
        state.tasks = state.tasks.filter(({ id }) => action.payload !== id);
      })
      .addCase(actions.createTask.fulfilled, (state, action) => {
        state.tasks = [...state.tasks, action.payload];
      })
      .addCase(actions.setTaskToEdit, (state, action) => {
        state.taskToEdit = action.payload.task;
      })
  }
);
