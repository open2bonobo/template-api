import { TodoItem } from "../types";

const url = 'https://localhost:7278/api/task'

export const fetchTasks = async (): Promise<TodoItem[]> =>
  await fetch(url).then((data) => data.json());

export const fetchUpdateTask = async ({ task }: { task: TodoItem }): Promise<TodoItem> => await fetch(`${url}/${task.id}`, {
  method: "PUT",
  headers: {
    "Content-type": "application/json"
  },
  body: JSON.stringify(task)
}).then((data) => data.json())

export const fetchCreateTask = async ({ task }: { task: TodoItem }): Promise<TodoItem> => await fetch(url, {
  method: "POST",
  headers: {
    "Content-type": "application/json"
  },
  body: JSON.stringify(task)
}).then((data) => data.json())

export const fetchDeleteTask = async ({ id }: { id: number }) => await fetch(`${url}/${id}`, {
  method: "DELETE",
})