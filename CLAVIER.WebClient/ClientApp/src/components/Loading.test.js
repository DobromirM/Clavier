import React from 'react';
import { Loading } from './Loading';
import ShallowRenderer from 'react-test-renderer/shallow';


it('check if it renders', () => {
    const renderer = new ShallowRenderer();
    renderer.render(<Loading />);
    const result = renderer.getRenderOutput();
    expect(result).not.toBeNull();
});
