import 'package:flutter/material.dart';

import '../models/task.dart';
import '../pages/current_list.dart';

class TaskItem extends StatelessWidget {
  var backgroundColor;

  TaskItem(
      {required this.todo,
      required this.onTaskChanged,
      required this.removeTask,
      })
      : super(key: ObjectKey(todo));

  final Task todo;
  final void Function(Task task) onTaskChanged;
  final void Function(Task task) removeTask;

  @override
  Widget build(BuildContext context) {
    return ListTile(
      onTap: () {
        onTaskChanged(todo);
      },
      leading: Checkbox(
        checkColor: Colors.greenAccent,
        activeColor: Colors.red,
        value: todo.completedAt != null,
        onChanged: (value) {
          onTaskChanged(todo);
        },
      ),
      //The leading property is a widget displayed at the beginning of the ListTile. In this case, it's a Checkbox that represents
      // the task's completion status. The value property is set based on whether todo.completedAt is not null. When the Checkbox
      //  is changed, it calls the onChanged callback, which also toggles the task's completion status.
      title: Row(children: <Widget>[
        // Inside the ListTile, there's a Row containing a Text widget that displays the task's name (todo.taskName).
        Expanded(
          //  The Expanded widget ensures that the text takes up as much horizontal space as possible.
          child: Text(todo.taskName),
        ),
        // IconButton(
        //   iconSize: 30,
        //   icon: const Icon(
        //     Icons.delete,
        //     color: Colors.red,
        //   ),
        //   alignment: Alignment.centerRight,
        //   onPressed: () {
        //     removeTask(todo);
        //   },
        // ),
        //In the same Row, there's an IconButton with a trash can icon (from Icons.delete). When pressed, it calls the removeTask callback to delete the task
        // The onTaskChanged and removeTask callbacks would be responsible for updating the task's status and removing it from the list, respectively.
      ]),
    );
  }
}
