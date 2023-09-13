import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:todo_tutorial/pages/completed_list.dart';
import '../models/task.dart';
import '../models/priority.dart' as TaskPriority;
import '../widgets/todo_item.dart';
import 'package:todo_tutorial/api/api_helpers.dart';

class TodoList extends StatefulWidget {
  const TodoList({super.key, required this.title});
  final String title;

  @override
  State<TodoList> createState() => TodoListState();
}

class TodoListState extends State<TodoList> {
  final TextEditingController _textFieldController = TextEditingController();
  bool refreshList = false;
  // we are including refresh list here so that we can trigger refreshList
  // whenever we do any of the actions like adding a new task or deleting one.

  TaskPriority.Priority defaultPriority = TaskPriority.Priority(id: 0, Name: 'Select a priority', Sequence: 0);
  TaskPriority.Priority? selectedPriority;
  List<TaskPriority.Priority> priorityList = <TaskPriority.Priority>[];

  getPriorityList() async {
    var response = await fetch('Public/GetPriorityList', null);
        if (response.statusCode == 200)
          {
            var decodedList = (jsonDecode(response.body) as List);
            setState(() {
              priorityList = decodedList.map((e) => (TaskPriority.Priority.fromJson(e))).toList();
              refreshList = true;
            });
          }
      }

  Future toggleTaskCompleted(Task toggledTask) async {
    dynamic response = await post('Public/ToggleCompleted', toggledTask);
    if (response.statusCode == 200) {
      setState(() {
        refreshList = true;
      });
    }
  }

  Future saveTask(Task task) async {
    dynamic response = await post('Public/SaveTask', task);
    if (response.statusCode == 200) {
      setState(() {
        refreshList = true;
      });
    }
  }

  Future deactivateTask(Task deletedTask) async {
    dynamic response = await post('Public/DeactivateTask', deletedTask);
    if (response.statusCode == 200) {
      setState(() {
        refreshList = true;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    refreshList = true;
    getTaskList(false);
    getPriorityList();
  }

  void _addTodoItem(String taskName) {
    var newTask = Task(
      taskName: taskName,
      createdAt: DateTime.now(),
      priorityId: selectedPriority!.Sequence,
    );
    saveTask(newTask).then((value) => {
          _textFieldController.clear(),
          setState(() {
            refreshList = true;
          })
        });
  }

  void handleTodoChange(Task todo) {
    toggleTaskCompleted(todo).then((value) => {
          setState(() {
            refreshList = true;
          }),
        });
  }

  void deleteTodo(Task todo) {
    deactivateTask(todo).then((value) => {
          setState(() {
            refreshList = true;
          })
        });
  }

  Color getColorForPriority(TaskPriority.Priority priority){
    switch (priority.Sequence){
      case 1:
        return Colors.green;
      case 2:
        return Colors.amber;
      case 3:
        return Colors.redAccent;
      default:
        return Colors.white;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text(widget.title),
        ),
        body: Column(
          children: <Widget>[
            Expanded(
              child: FutureBuilder(
                  future: getTaskList(false),
                  builder: (context, snapshot) {
                    if (snapshot.connectionState == ConnectionState.waiting) {
                      return const CircularProgressIndicator();
                    }
                    if (snapshot.hasData) {
                      var todos = snapshot.data!;
                      todos.sort((a, b) => b.priorityId.compareTo(a.priorityId));
                      return ListView.builder(
                        itemCount: todos.length,
                        itemBuilder: (context, index) {
                          var todo = todos.elementAt(index);
                          var backgroundColor = getColorForPriority(priorityList.firstWhere((element) => element.Sequence == todo.priorityId));
                          return Card(
                            color: backgroundColor,
                            child: Dismissible(
                              key: Key(todo.taskName),
                              onDismissed: (DismissDirection direction) {
                                if (direction == DismissDirection.startToEnd) {
                                  setState(() {
                                    deleteTodo(todo);
                                  });
                                  ScaffoldMessenger.of(context).showSnackBar(
                                      SnackBar(
                                          content: Text(
                                              '${todo.taskName} dismissed.')));
                                } else {
                                  setState(() {
                                    handleTodoChange(todo);
                                  });
                                  ScaffoldMessenger.of(context)
                                      .showSnackBar(SnackBar(
                                    content:
                                        Text('${todo.taskName} completed!'),
                                  ));
                                }
                              },
                              background: const ColoredBox(
                                color: Colors.red,
                              ),
                              secondaryBackground:
                                  const ColoredBox(color: Colors.green),
                              child: TaskItem(
                                  todo: todo,
                                  onTaskChanged: handleTodoChange,
                                  removeTask: deleteTodo
                              ),
                            ),
                          );
                        },
                      );
                    }
                    return Container(); //Handle errors or no items
                  }),
            ),
            ElevatedButton(
              style: ElevatedButton.styleFrom(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
              onPressed: () async {
                await Navigator.push(context,
                    MaterialPageRoute(builder: (context) => SecondRoute()));
              },
              child: const Text('To Completed Page'),
            ),
            FloatingActionButton(
              onPressed: () => _displayDialog(),
              tooltip: 'Add a Todo',
              child: const Icon(Icons.add),
            ),
          ],
        ));
  }

  void _displayDialog() {
    showDialog<void>(
      context: context,
      barrierDismissible: false,
      builder: (context) {
        return AlertDialog(
          //AlertDialog is how we will be showing the modal to add a new task
          title: const Text('Add a todo'),
          content: TextField(
            controller: _textFieldController,
            decoration:
                const InputDecoration(hintText: 'Add an item to your list'),
            autofocus: true,
          ),
          actions: <Widget>[
            DropdownButton<TaskPriority.Priority>(
              value: selectedPriority,
              onChanged: (TaskPriority.Priority? newValue) {
                setState(() {
                  selectedPriority = newValue!;
                });
              },
              items: priorityList.map<DropdownMenuItem<TaskPriority.Priority>>((priority) {
                return DropdownMenuItem<TaskPriority.Priority>(
                  value: priority,
                  child: Text(
                      priority.Name,
                      style: const TextStyle(color: Colors.black),
                  ),
                );
              }).toList(),
            ),

            OutlinedButton(
              style: OutlinedButton.styleFrom(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text('Cancel'),
            ),
            ElevatedButton(
              style: ElevatedButton.styleFrom(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
              onPressed: () {
                _addTodoItem(_textFieldController.text);
                Navigator.of(context).pop();
              },
              child: const Text('Add'),
            ),

          ],
          );
          },
        );
  }
}