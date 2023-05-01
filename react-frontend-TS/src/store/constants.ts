import { TodoPriority, TodoStatus } from "../types";

export const GET_TASKS = "tasks/fetchTasks"
export const DELETE_TASK = "tasks/deleteTask"
export const UPDATE_TASK = "tasks/updateTask"
export const UPDATE_TASK_TO_EDIT = "tasks/updateTask"
export const CREATE_TASK = "tasks/createTask"



export const todoPriorityLabelsMap: Record<TodoPriority, string> = {
  0: 'Low',
  1: "Medium low",
  2: "Medium",
  3: "Medium height",
  4: "Height",
}

export const todoStatusLabelsMap: Record<TodoStatus, string> = {
  0: 'Initial',
  1: "In progress",
  2: "Completed",
}