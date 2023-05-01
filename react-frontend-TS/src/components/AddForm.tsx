import React, { useState } from "react";
import Button from "./Button";
import AddTask from "./AddTask";


const AddForm = () => {
  const [showAddTask, setShowAddTask] = useState(false);
  const onClick = () => {
    setShowAddTask(!showAddTask);
  };
  return (
    <header className="container">
      <Button
        showAddTask={showAddTask}
        onClick={onClick}
      />
      {showAddTask && (
        <AddTask />
      )}
    </header>
  );
};

export default AddForm;
