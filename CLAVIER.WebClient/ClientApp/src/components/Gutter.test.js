import React from 'react';
import { Gutter } from './Gutter';
import ShallowRenderer from 'react-test-renderer/shallow';


it('check if it renders', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<Gutter />);
    const result = renderer.getRenderOutput();
    expect(result).not.toBeNull();
});

it('check if it renders children', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<Gutter />);
    const result = renderer.getRenderOutput();
    expect(result.props.children).not.toBeNull();
});
