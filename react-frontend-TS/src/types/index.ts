export type TodoPriority = 0 | 1 | 2 | 3 | 4

export type TodoStatus = 0 | 1 | 2

export type TodoItem = {
  id: number,
  name: string,
  description: string,
  priority: TodoPriority,
  status: TodoStatus
}

export type RootStore = {
  tasks: TodoItem[]
  taskToEdit: TodoItem,
  isShowAddForm: boolean,
}

