import { createAsyncThunk, createAction } from "@reduxjs/toolkit";
import { GET_TASKS, DELETE_TASK, UPDATE_TASK, CREATE_TASK, UPDATE_TASK_TO_EDIT } from "./constants";
import { TodoItem } from "../types";
import { fetchTasks, fetchDeleteTask, fetchUpdateTask, fetchCreateTask } from "../api";


export const actions = {
  getTasks: createAsyncThunk<
    TodoItem[]
    >(
    GET_TASKS,
    async (_, { rejectWithValue } ) => {
      try {
        return await fetchTasks();
      } catch (e: any) {
        return rejectWithValue(e.response.data);
      }
    },
  ),
   deleteTask: createAsyncThunk<number, { id: number }>(DELETE_TASK, async ({ id }, { rejectWithValue } ) => {
     try {
       await fetchDeleteTask({ id });
       return id
     } catch (e: any) {
       return rejectWithValue(e.response.data);
     }
   }),
  createTask: createAsyncThunk<TodoItem, { task: Exclude<TodoItem, TodoItem["id"]> }>(CREATE_TASK, async ({ task }, { rejectWithValue } ) => {
    try {
      return await fetchCreateTask({ task });
    } catch (e: any) {
      return rejectWithValue(e.response.data);
    }
  }),
  updateTask: createAsyncThunk<TodoItem, { task: TodoItem }>(UPDATE_TASK, async ({ task }, { rejectWithValue } ) => {
    try {
      return await fetchUpdateTask({ task });
    } catch (e: any) {
      return rejectWithValue(e.response.data);
    }
  }),
  setTaskToEdit: createAction<{ task: TodoItem }>(UPDATE_TASK_TO_EDIT)
}
