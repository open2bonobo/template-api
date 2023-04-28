import React from "react"
import AddForm from "./AddForm";
import ListView from "./ListView";
import { useState } from "react";

const TodoComponent = () => {
    const [tasks, setTasks] = useState([
        {
          id: 1,
          name: "Woshing",
          description: "i am here 0",
          priority: 1,
          status: 2,
        },
        {
          id: 3,
          name: "Breakfast",
          description: "i am here 2",
          priority: 1,
          status: 0,
        },
        {
          id: 4,
          name: "HorsingRound",
          description: "i am here 3",
          priority: 1,
          status: 0,
        },
      ]);


  return (
    <div className="container">
        <AddForm />
        <ListView tasks={tasks}/>
    </div>
  )
}

export default TodoComponent;