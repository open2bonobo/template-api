import React, { useState } from "react";
import Button from "./Button";
import AddTask from "./AddTask";
import { RootStore } from "../types";
import { useDispatch, useSelector } from "react-redux";
import { actions } from "../store/actions";

const AddForm = () => {
  const isShowAddForm = useSelector((store: RootStore) => store.isShowAddForm);
  const dispatch = useDispatch();
  const onClick = () => {
    dispatch(actions.setShowAddForm(!isShowAddForm));
  };
  return (
    <header className="row justify-content-center">
      <div className="col-6">
        <Button showAddTask={isShowAddForm} onClick={onClick} />
        {isShowAddForm && <AddTask />}
      </div>
    </header>
  );
};

export default AddForm;
function dispatch(arg0: any) {
  throw new Error("Function not implemented.");
}
