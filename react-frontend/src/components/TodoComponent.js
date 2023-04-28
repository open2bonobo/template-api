import React from "react";
import AddForm from "./AddForm";
import ListView from "./ListView";
import { useState, useEffect } from "react";

const TodoComponent = () => {
  const url = "https://localhost:7278/api/task";
  const optionsPriority = [
    { value: 1, label: "Low" },
    { value: 2, label: "Medium-Low" },
    { value: 3, label: "Medium" },
    { value: 4, label: "Medium-High" },
    { value: 5, label: "High" },
  ];
  const optionsStatus = [
    { value: 0, label: "Initial" },
    { value: 1, label: "InProgress" },
    { value: 2, label: "Completed" },
  ];
  const defaultFormData = {
    id: 0,
    name: "default",
    description: "default",
    priority: optionsPriority[0].value,
    status: optionsStatus[0].value,
  };
  const [tasks, setTasks] = useState([]);
  const [taskToEdit, setTaskToEdit] = useState(defaultFormData);

  useEffect(() => {
    const getTasks = async () => {
      const tasksFromServer = await fetchTasks();
      setTasks(tasksFromServer);
    };

    getTasks();
  }, []);

  // Fetch Tasks
  const fetchTasks = async () => {
    const res = await fetch(url);
    const data = await res.json();

    return data;
  };

  const onEdit = (task) => {
    setTaskToEdit(task);
  };
  const deleteTask = async (id) => {
    console.log("DELETE", id);
    const res = await fetch(`${url}/${id}`, {
      method: "DELETE",
    });
    res.status === 200
      ? setTasks(tasks.filter((task) => task.id !== id))
      : alert("Error Deleting This Task");
  };
  const onEditData = async (task) => {
    const newTask = {
      id: taskToEdit.id,
      name: task.name,
      description: task.description,
      priority: task.prioritySelectedOption.value,
      status: task.statusSelectedOption.value,
    };
    const res = await fetch(`${url}/${taskToEdit.id}`, {
      method: "PUT",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(newTask),
    });

    const data = await res.json();
    const updatedTodos = tasks.map((todo) => {
      if (todo.id === 0) {
        return {
          id: data.id,
          name: data.name,
          description: data.description,
          priority: data.priority,
          status: data.status,
        };
      }
      return todo;
    });
    setTasks(updatedTodos);
    setTaskToEdit(defaultFormData);
  };
  const addTask = async (task) => {
    const newTask = {
      name: task.name,
      description: task.description,
      priority: task.prioritySelectedOption.value,
      status: task.statusSelectedOption.value,
    };
    const res = await fetch(url, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(newTask),
    });

    const data = await res.json();

    setTasks([...tasks, data]);
  };

  return (
    <div className="container">
      <AddForm
        onAdd={addTask}
        optionsPriority={optionsPriority}
        optionsStatus={optionsStatus}
        taskToEdit={taskToEdit}
        onEditData={onEditData}
      />
      <ListView
        tasks={tasks}
        onDelete={deleteTask}
        onEdit={onEdit}
        optionsPriority={optionsPriority}
        optionsStatus={optionsStatus}
      />
    </div>
  );
};

export default TodoComponent;
