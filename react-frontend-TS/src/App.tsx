import React from 'react';
import './App.css';
import AddForm from "./components/AddForm";
import ListView from "./components/ListView";


export const App = () => {
  return (
    <div className="container">
      <AddForm />
      <ListView />
    </div>
  );
};

export default App;

