import React from "react";
import { useState, useEffect } from "react";

const AddTask = ({ onAdd,onEditData, optionsPriority, optionsStatus, taskToEdit }) => {
  const [name, setName] = useState("default");
  const [description, setDescription] = useState("default");
  const [prioritySelectedOption, setPrioritySelectedOption] = useState(
    optionsPriority[0]
  );
  const [statusSelectedOption, setStatusSelectedOption] = useState(
    optionsStatus[0]
  );
  useEffect(() => {
    console.log(taskToEdit);
    if (taskToEdit) {
      setName(taskToEdit.name);
      setDescription(taskToEdit.description);

      const prioritySelectedOption = optionsPriority.find(
        (option) => option.value == taskToEdit.priority
      );
      setPrioritySelectedOption(prioritySelectedOption);
      const statusSelectedOption = optionsStatus.find(
        (option) => option.value == taskToEdit.status
      );
      setStatusSelectedOption(statusSelectedOption);
    }
  }, [taskToEdit]);
  function handlePriorityDropdownChange(event) {
    const selectedValue = event.target.value;
    const prioritySelectedOption = optionsPriority.find(
      (option) => option.value == selectedValue
    );
    setPrioritySelectedOption(prioritySelectedOption);
  }
  function handleStatusDropdownChange(event) {
    const selectedValue = event.target.value;
    const statusSelectedOption = optionsStatus.find(
      (option) => option.value == selectedValue
    );
    setStatusSelectedOption(statusSelectedOption);
  }

  const onSubmit = (e) => {
    e.preventDefault();
    if (!name || !description) {
      alert("ENTER SMTH IN THE INPUTS");
      return;
    }
    if (taskToEdit.id == 0) {
      onAdd({
        name,
        description,
        prioritySelectedOption,
        statusSelectedOption,
      });
    } else {
      onEditData({
        name,
        description,
        prioritySelectedOption,
        statusSelectedOption,});
      console.log("save changes");
    }
    setName("default");
    setDescription("default");
    setPrioritySelectedOption(optionsPriority[0]);
    setStatusSelectedOption(optionsStatus[0]);
    taskToEdit.id = 0;
  };
  return (
    <form className="add-form" onSubmit={onSubmit}>
      <div className="form-control">
        <label>Name</label>
        <input
          type="text"
          placeholder="Add Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        ></input>
      </div>
      <div className="form-control">
        <label>Description</label>
        <input
          type="text"
          placeholder="Add Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        ></input>
      </div>
      <div>
        <label htmlFor="dropdown">Select an option:</label>
        <select
          id="dropdown"
          value={prioritySelectedOption.value}
          onChange={handlePriorityDropdownChange}
        >
          {optionsPriority.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
        <p>You selected: {prioritySelectedOption.label}</p>
      </div>
      <div>
        <label htmlFor="dropdown">Select an option:</label>
        <select
          id="dropdown"
          value={statusSelectedOption.value}
          onChange={handleStatusDropdownChange}
        >
          {optionsStatus.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
        <p>You selected: {statusSelectedOption.label}</p>
      </div>
      <input type="submit" value="Save Task" className="btn btn-block"></input>
    </form>
  );
};

export default AddTask;
