import React from "react";
import PropTypes from "prop-types";
import Button from "./Button";

const AddForm = ({ title }) => {
  const onClick = () => {
    console.log("clicked");
  };
  return (
    <header className="header">
      <h1>{title}</h1>
      <Button color="green" text="Add" onClick={onClick} />
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
