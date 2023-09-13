import 'dart:convert';

import 'package:flutter/material.dart';

import 'package:todo_tutorial/pages/current_list.dart';

import '../models/task.dart';
import '../widgets/navbar.dart';
import '../widgets/todo_item.dart';
import 'package:todo_tutorial/api/api_helpers.dart';
import 'package:flutter_slidable/flutter_slidable.dart';

class SecondRoute extends StatefulWidget {
  const SecondRoute({super.key});
  //final Task? todo;
  @override
  State<SecondRoute> createState() => SecondRouteState();
}
//final TodoListState tdList = TodoListState();
bool refreshList = false;



class SecondRouteState extends State<SecondRoute>{
  bool refreshList = false;

  Future deactivateTask(Task deletedTask) async {
    dynamic response = await post('Public/DeactivateTask', deletedTask);
    if (response.statusCode == 200) {
      setState(() {
        refreshList = true;
      });
    }
  }

  void deleteTodo(Task todo) {
    deactivateTask(todo).then((value) => {
      setState(() {
        refreshList = true;
      })
    });
  }


  Future toggleTaskCompleted(Task toggledTask) async {
    dynamic response = await post('Public/ToggleCompleted', toggledTask);
    if (response.statusCode == 200) {
      setState(() {
        refreshList = true;
      });
    }
  }

  void handleTodoChange(Task todo) {
    toggleTaskCompleted(todo).then((value) => {
      setState(() {
        refreshList = true;
      }),

      //   Navigator.push(
      //     context, MaterialPageRoute(builder:
      //     (context) => SecondRoute(todo: todo))
      // )
    });
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Completed Route'),
      ),
      body: Column(
          children: <Widget> [

            Expanded(

              child: FutureBuilder(
                // FutureBuilders allows you to create widgets that depend on the completion of a Future and
                // automatically rebuilds your UI when the Future completes or changes its state.
                  future: getTaskList(true),
                  //declaring the future that will cause it to rebuild.
                  builder: (context, snapshot) {
                    if (snapshot.connectionState == ConnectionState.waiting) {
                      return const CircularProgressIndicator();
                    }
                    if (snapshot.hasData) {
                      var todos = snapshot.data!;
                      return ListView.builder(
                        itemCount: todos?.length,
                        itemBuilder: (context, index) {
                          var todo = todos.elementAt(index);
                          return Dismissible(
                            key: Key(todo.taskName),
                            onDismissed: (DismissDirection direction) {
                              if (direction == DismissDirection.startToEnd) {
                                setState(() {
                                  deleteTodo(todo);
                                });
                                ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('${todo.taskName} dismissed.')));
                              }
                              else
                              {
                                setState(() {
                                  handleTodoChange(todo);
                                });
                                ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('${todo.taskName} un-completed!'),));
                              }
                            },
                            background: const ColoredBox(
                                color: Colors.red,
                                child: Align(
                                  alignment: Alignment.centerLeft,
                                  child: Padding(
                                      padding: EdgeInsets.all(16.0),
                                      child: Icon(Icons.delete, color: Colors.white),
                                  ),
                                ),
                            ),
                            secondaryBackground: const ColoredBox(
                                color: Colors.orange
                            ),
                            child: TaskItem(
                                todo: todo,
                                onTaskChanged: handleTodoChange,
                                removeTask: deleteTodo),
                          );
                        },
                      );
                    }
                    return Container(); //Handle errors or no items
                  }),
            ),

            ElevatedButton(
              onPressed: () {
                // Navigate back to first route when tapped.
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => TodoList(title: 'title')),
                );
              },
              child: const Text('Go back!'),
            ),
          ]
      ),
    );
  }}