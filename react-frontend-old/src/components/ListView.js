import React from "react";
import Tasks from "./Tasks";

const ListView = ({ tasks, onDelete, onEdit, optionsPriority, optionsStatus }) => {
  return (
    <div>
      {tasks.length > 0 ? (
        <Tasks
          tasks={tasks}
          onDelete={onDelete}
          onEdit={onEdit}
          optionsPriority={optionsPriority}
          optionsStatus={optionsStatus}
        />
      ) : (
        "No Tasks exist"
      )}
    </div>
  );
};

export default ListView;
