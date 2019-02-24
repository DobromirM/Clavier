import React from 'react';
import { NoteEditor } from './NoteEditor';
import ShallowRenderer from 'react-test-renderer/shallow';


it('check if it renders', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<NoteEditor />);
    const result = renderer.getRenderOutput();
    expect(result).not.toBeNull();
});

it('check if it renders children', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<NoteEditor />);
    const result = renderer.getRenderOutput();
    expect(result.props.children).not.toBeNull();
});

it('check if the should component update function is defined', () => {
    expect(NoteEditor.prototype.shouldComponentUpdate).not.toBeNull();
});

it('check if the update notes function is defined', () => {
    expect(NoteEditor.prototype.updateNotes).not.toBeNull();
});

it('check if the render notes function is defined', () => {
    expect(NoteEditor.prototype.renderNotes).not.toBeNull();
});

it('check if the count lines function is defined', () => {
    expect(NoteEditor.prototype.countLines).not.toBeNull();
});