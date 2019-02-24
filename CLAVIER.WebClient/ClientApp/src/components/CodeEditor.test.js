import React from 'react';
import { CodeEditor } from './CodeEditor';
import ShallowRenderer from 'react-test-renderer/shallow';


it('check if it renders', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<CodeEditor />);
    const result = renderer.getRenderOutput();
    expect(result).not.toBeNull();
});

it('check if it renders children', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<CodeEditor />);
    const result = renderer.getRenderOutput();
    expect(result.props.children).not.toBeNull();
});

it('check if the update code function is defined', () => {
    expect(CodeEditor.updateCode).not.toBeNull();
});

it('check if the render code function is defined', () => {
    expect(CodeEditor.renderCode).not.toBeNull();
});

it('check if the create markers function is defined', () => {
    expect(CodeEditor.prototype.createMarkers).not.toBeNull();
});

it('check if the highlight function is defined', () => {
    expect(CodeEditor.highlight).not.toBeNull();
});

it('test the render code function', () => {
    expect(CodeEditor.renderCode(["This", "is", "test"])).toEqual("This\nis\ntest");
});

it('test the create markers function', () => {
    expect(CodeEditor.prototype.createMarkers([1])).toEqual([{ "className": "yellow-marker", "endRow": 1, "startRow": 0, "type": "background" }]);

    expect(CodeEditor.prototype.createMarkers([1, 3, 5])).toEqual([{ "className": "yellow-marker", "endRow": 1, "startRow": 0, "type": "background" },
                                                                   { "className": "yellow-marker", "endRow": 3, "startRow": 2, "type": "background" },
                                                                   { "className": "yellow-marker", "endRow": 5, "startRow": 4, "type": "background" }]);
});