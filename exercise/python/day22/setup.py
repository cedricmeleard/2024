import assertpy
from setuptools import setup, find_packages

setup(
    name='Tour Calculator',
    version='0.1',
    packages=find_packages(),
    install_requires=[
        assertpy
    ],
    test_suite='tests',  # run them with pytest --hypothesis-show-statistics
)