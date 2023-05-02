import Task from "./Task";
import { defaultTask } from "../store/reducer";
import { fireEvent, render, screen } from "@testing-library/react";

describe("Task", () => {
  it("Should render data correctly", () => {
    //Assign
    const onDelete = jest.fn();
    const onEdit = jest.fn();
    render(<Task task={defaultTask} onDelete={onDelete} onEdit={onEdit} />);
    //Act
    expect(screen.getByText('Name: default')).toBeInTheDocument();
    expect(screen.getByText('Description: default')).toBeInTheDocument();
  });
});