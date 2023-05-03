import React from "react";

type Props = {
  showAddTask: boolean;
  onClick: () => void;
};

const Button = ({ showAddTask, onClick }: Props) => {
  return (
    <button
      onClick={onClick}
      className={`btn col-12 my-3 ${showAddTask ? "btn-danger" : "btn-primary"}`}
    >
      {showAddTask ? "Close" : "Add"}
    </button>
  );
};

export default Button;
