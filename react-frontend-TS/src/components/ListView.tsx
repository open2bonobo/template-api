import React, { useCallback, useEffect } from "react";
import Task from "./Task";
import { actions } from "../store/actions";
import { useAppDispatch } from "../store";
import { RootStore, TodoItem } from "../types";
import { useSelector } from "react-redux";

const ListView = () => {
  const dispatch = useAppDispatch()
  const tasks = useSelector((store: RootStore) => store.tasks)

  useEffect(() => {
    dispatch(actions.getTasks())
  }, [dispatch]);

  const onDelete = useCallback((id: number) => {
    dispatch(actions.deleteTask({ id }))
  }, [dispatch])

  const onEdit = useCallback((task: TodoItem) => {
    dispatch(actions.setTaskToEdit({ task }))
  }, [dispatch])


  return (
    <div>
      {tasks.length ? (
        tasks.map((task) => (
            <Task
              key={task.id}
              task={task}
              onDelete={onDelete}
              onEdit={onEdit}
            />
          ))
      ) : (
        "No Tasks exist"
      )}
    </div>
  );
};

export default ListView;
