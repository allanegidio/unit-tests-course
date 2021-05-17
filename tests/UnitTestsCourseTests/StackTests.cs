using System;
using FluentAssertions;
using UnitTestsCourse;
using Xunit;

namespace UnitTestsCourseTests
{
    public class StackTests
    {
        [Fact]
        public void Push_ArgIsNull_ThrowArgumentNullException()
        {
            var stack = new Stack<string>();

            Action action = () => stack.Push(null);

            action.Should()
                    .Throw<ArgumentNullException>();
        }

        [Fact]
        public void Push_ValidArg_AddToTheStack()
        {
            var stack = new Stack<string>();

            stack.Push("First Item");

            stack.Count.Should().Be(1);
        }

        [Fact]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Stack<string>();

            stack.Count.Should().Be(0);
        }

        [Fact]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Action action = () => stack.Pop();

            action.Should()
                    .Throw<InvalidOperationException>();
        }

        [Fact]
        public void Pop_StackWithAFewObjects_ReturnObjectOnTheTop()
        {
            //Arrage
            var stack = new Stack<string>();
            stack.Push("First Item");
            stack.Push("Second Item");

            //Act
            var result = stack.Pop();

            //Assert
            result.Should()
                    .Be("Second Item");
        }

        [Fact]
        public void Pop_StackWithAFewObjects_RemoveObjectOnTheTop()
        {
            //Arrage
            var stack = new Stack<string>();
            stack.Push("First Item");
            stack.Push("Second Item");
            stack.Push("Third Item");

            //Act
            stack.Pop();

            //Assert
            stack.Count.Should()
                        .Be(2);
        }

        [Fact]
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Action action = () => stack.Peek();

            action.Should()
                    .Throw<InvalidOperationException>();
        }

        [Fact]
        public void Peek_StackWithAFewObjects_ReturnObjectOnTheTop()
        {
            //Arrage
            var stack = new Stack<string>();
            stack.Push("First Item");
            stack.Push("Second Item");
            stack.Push("Third Item");

            //Act
            var result = stack.Peek();

            //Assert
            result.Should()
                    .Be("Third Item");
        }

    }
}