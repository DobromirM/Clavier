import React from 'react';
import { OpenButton } from './OpenButton';
import ShallowRenderer from 'react-test-renderer/shallow';


it('check if it renders', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<OpenButton />);
    const result = renderer.getRenderOutput();
    expect(result).not.toBeNull();
});

it('check if it renders children', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<OpenButton />);
    const result = renderer.getRenderOutput();
    expect(result.props.children).not.toBeNull();
});

it('check if the reroute click function is defined', () => {
    expect(OpenButton.prototype.rerouteClick).not.toBeNull();
});

it('check if the readFile function is defined', () => {
    expect(OpenButton.prototype.readFile).not.toBeNull();
});

it('check if the displayFile function is defined', () => {
    expect(OpenButton.prototype.displayFile).not.toBeNull();
});