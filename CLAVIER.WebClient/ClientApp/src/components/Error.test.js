import React from 'react';
import { Error } from './Error';
import ShallowRenderer from 'react-test-renderer/shallow';


it('check if it renders', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<Error />);
    const result = renderer.getRenderOutput();
    expect(result).not.toBeNull();
});
