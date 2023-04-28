import React from 'react'
import Tasks from './Tasks'

const ListView = ({tasks}) => {
  return (
    <div>ListView
        <Tasks tasks={tasks}/>
    </div>
  )
}

export default ListView