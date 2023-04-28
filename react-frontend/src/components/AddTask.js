import React from "react";
import { useState, useEffect } from "react";

const AddTask = ({
  onAdd,
  onEditData,
  optionsPriority,
  optionsStatus,
  taskToEdit,
}) => {
  const [errorNameMessage, setNameErrorMessage] = useState("");
  const [errorDescriptionMessage, setDescriptionErrorMessage] = useState("");
  const [name, setName] = useState("default");
  const [description, setDescription] = useState("default");
  const [prioritySelectedOption, setPrioritySelectedOption] = useState(
    optionsPriority[0]
  );
  const [statusSelectedOption, setStatusSelectedOption] = useState(
    optionsStatus[0]
  );
  function validateNameInput(input) {
    const regex = /^[a-zA-Z0-9\s]*$/;
    return input.length <= 50 && regex.test(input);
  }
  function validateDescriptionInput(input) {
    const regex = /^[a-zA-Z0-9\s]*$/;
    return input.length <= 500 && regex.test(input);
  }
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
  function handleInputNameChange(event) {
    const value = event.target.value;
    if (validateNameInput(value)) {
      setName(value);
      setNameErrorMessage("");
    } else {
      setNameErrorMessage(
        "Input should only contain text, alphabet symbols, and numbers, and be no longer than 50 characters."
      );
    }
  }
  function handleInputDescriptionChange(event) {
    const value = event.target.value;
    if (validateDescriptionInput(value)) {
      setDescription(value);
      setDescriptionErrorMessage("");
    } else {
      setDescriptionErrorMessage(
        "Input should only contain text, alphabet symbols, and numbers, and be no longer than 500 characters."
      );
    }
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
        statusSelectedOption,
      });
      console.log("save changes");
    }
    setName("default");
    setDescription("default");
    setPrioritySelectedOption(optionsPriority[0]);
    setStatusSelectedOption(optionsStatus[0]);
    taskToEdit.id = 0;
  };
  return (
    <form onSubmit={onSubmit}>
      <div className="form-control">
        <label for="formName">Name</label>
        <input
          id="formName"
          type="text"
          placeholder="Add Name"
          value={name}
          onChange={handleInputNameChange}
          required
        ></input>
        {errorNameMessage && <p style={{ color: "red" }}>{errorNameMessage}</p>}
      </div>
      <div className="form-control">
        <label for="formDescription">Description</label>
        <input
          id="formDescription"
          type="text"
          placeholder="Add Description"
          value={description}
          onChange={handleInputDescriptionChange}
          required
        ></input>
        {errorDescriptionMessage && <p style={{ color: "red" }}>{errorDescriptionMessage}</p>}
      </div>
      <div className="form-control">
        <label>Select an option:</label>
        <select
          className="btn"
          value={prioritySelectedOption.value}
          onChange={handlePriorityDropdownChange}
        >
          {optionsPriority.map((option) => (
            <option
              className="dropdown-item"
              key={option.value}
              value={option.value}
            >
              {option.label}
            </option>
          ))}
        </select>
        <p>You selected: {prioritySelectedOption.label}</p>
      </div>
      <div className="form-control">
        <label>Select an option:</label>
        <select
          className="btn"
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
