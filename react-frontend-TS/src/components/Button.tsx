import React from "react";

type Props = {
  showAddTask: boolean
  onClick: () => void
}

const Button = ({ showAddTask, onClick }: Props) => {
  return (
    <button className="btn" onClick={onClick} style={{ backgroundColor: showAddTask ? "red" : "green" }}>
      {showAddTask ? "Close" : "Add"}
    </button>
  );
};

export default Button;
