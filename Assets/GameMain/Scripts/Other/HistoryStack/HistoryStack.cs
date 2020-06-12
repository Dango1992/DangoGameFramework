using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;

namespace Dango.Other
{
    public class HistoryStack : IReference
    {
        private List<ICommand> undoStack = new List<ICommand>();
        private List<ICommand> redoStack = new List<ICommand>();
        
        private int historyCount = 10;

        public int UndoCount
        {
            get { return undoStack.Count; }
        }

        public int RedoCount
        {
            get { return redoStack.Count; }
        }

        public bool hasUndoCommand
        {
            get { return UndoCount > 0; }
        }

        public bool hasRedoCommand
        {
            get { return RedoCount > 0; }
        }

        public bool isHistoryOverflow
        {
            get { return UndoCount > historyCount; }
        }

        public HistoryStack()
        {
            
        }

        public HistoryStack(int historyCount)
        {
            ChangeHistoryCount(historyCount);
        }

        public void ChangeHistoryCount(int historyCount)
        {
            this.historyCount = historyCount;
        }

        public void Clear()
        {
            UndoStackClear();
            RedoStackClear();
        }
        
        private void UndoStackClear()
        {
            foreach (var cmd in undoStack)
            {
                Release(cmd);
            }
            
            undoStack.Clear();
        }

        private void RedoStackClear()
        {
            foreach (var cmd in redoStack)
            {
                Release(cmd);
            }
            
            redoStack.Clear();
        }


        public void Do(ICommand cmd)
        {
            cmd.Do();
            
            RedoStackClear();
            undoStack.Add(cmd);
            OverflowCheck();
        }

        public void Undo()
        {
            if (hasUndoCommand)
            {
                ICommand cmd = undoStack[UndoCount - 1];

                cmd.Undo();
                undoStack.Remove(cmd);
                redoStack.Add(cmd);
            }
        }

        public void Redo()
        {
            if (hasRedoCommand)
            {
                ICommand cmd = redoStack[RedoCount - 1];

                cmd.Do();
                redoStack.Remove(cmd);
                undoStack.Add(cmd);
            }
        }
        
        private void OverflowCheck()
        {
            if (isHistoryOverflow)
            {
                Debug.Log("CHECK");
                undoStack.RemoveAt(0);
                Release(undoStack[0]);
            }
        }

        /// <summary>
        /// 从对象池获取实例化命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Spwaner<T>() where T : class,ICommand,new()
        {
            return new T();
        }

        /// <summary>
        /// 命令回收
        /// </summary>
        /// <param name="cmd"></param>
        /// <typeparam name="T"></typeparam>
        public static void Release<T>(T cmd) where T : ICommand
        {
            ReferencePool.Release(cmd);
        }
    }
}