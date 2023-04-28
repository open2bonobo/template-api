import React, { useState } from "react";
import PropTypes from "prop-types";
import Button from "./Button";
import AddTask from "./AddTask";

const AddForm = ({ onAdd, onEditData, optionsPriority, optionsStatus, taskToEdit }) => {
  const [showAddTask, setShowAddTask] = useState(false);
  const onClick = () => {
    console.log("clicked");
    setShowAddTask(!showAddTask);
  };
  return (
    <header className="header">
      <Button
        color={showAddTask ? "red" : "green"}
        text={showAddTask ? "Close" : "Add"}
        onClick={onClick}
      />
      <div className="container">
        {showAddTask && (
          <AddTask
            onAdd={onAdd}
            optionsPriority={optionsPriority}
            optionsStatus={optionsStatus}
            taskToEdit={taskToEdit}
            onEditData={onEditData}
          />
        )}
      </div>
    </header>
  );
};
AddForm.defaultProps = {
  title: "TODO Viewer",
};
AddForm.defaultProps = {
  title: PropTypes.string.isRequired,
};

export default AddForm;
