import React from 'react';
import { NotesButton } from './NotesButton';
import ShallowRenderer from 'react-test-renderer/shallow';


it('check if it renders', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<NotesButton />);
    const result = renderer.getRenderOutput();
    expect(result).not.toBeNull();
});

it('check if it renders children', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<NotesButton />);
    const result = renderer.getRenderOutput();
    expect(result.props.children).not.toBeNull();
});

it('check if the toggle status function is defined', () => {
    expect(NotesButton.prototype.toggleStatus).not.toBeNull();
});